package Messages.RESTobjects;

import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.lang.reflect.Type;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

public abstract class AbstractRESTOperation implements RESTOperation {

    private String uri;
    private String body;
    private boolean isVerbose;

    public AbstractRESTOperation() {
        uri = null;
        body = null;
        isVerbose = false;
    }

    protected String GetURI() { return uri; }
    protected String GetBody() { return body; }

    /**
     * Prints the given message only if this operation is verbose.
     * @param msg Message to (maybe) print.
     */
    protected void DebugMsg(String msg) {
        if(IsVerbose()) {
            System.out.println(msg);
        }
    }

    /**
     * Checks if verbose.
     *
     * That is, if we are very talkative. I don't know about you,
     * but this comment feels like chatting. The other day I tripped over a puddle
     * and fell into a brick. Though it really hurt, it didn't hurt quite as much
     * as the doctor's paycheck. So I asked to see a financial doctor, but I was
     * told that they had all fallen into a depression.
     * @return True if we're verbose.
     */
    protected boolean IsVerbose() {
        return isVerbose;
    }

    @Override
    public final RESTOperation Body(String jsonBody) {
        body = jsonBody;
        return this;
    }

    @Override
    public final RESTOperation URI(String source) {
        uri = source;
        return this;
    }

    @Override
    public final RESTOperation URIPostfix(String postfix) {

        if(uri == null) {
            throw new IllegalStateException("Cannot append to null URI");
        }

        uri = String.join("/", uri, postfix);
        return this;
    }

    @Override
    public RESTOperation Verbose() {
        isVerbose = true;
        return this;
    }

    @Override
    public final String Execute() {
        String result = null;

        if(uri == null) {
            throw new IllegalStateException("Cannot execute REST operation without a URI");
        }

        try {
            URL url = new URL(GetURI());
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();

            SetupConnection(conn);

            if (conn.getResponseCode() != 200) {
                throw new RuntimeException("Failed : REST error code : "
                        + conn.getResponseCode());
            }

            BufferedReader br = new BufferedReader(new InputStreamReader(
                    conn.getInputStream()
            ));

            String output;
            DebugMsg("\nOutput from Server ....");
            String json= "";
            while ((output = br.readLine()) != null) {
                //System.out.println(output);
                json += output;
            }
            //System.out.println(output);
            DebugMsg(json);
            conn.disconnect();

            result = json;
        }
        catch ( MalformedURLException e) {
            e.printStackTrace();
        }
        catch ( IOException e) {
            e.printStackTrace();
        }

        return result;
    }

    @Override
    public final <T> T ExecuteAndConvert(Class<T> classOfT) {
        T result = null;
        String json = Execute();

        if(json != null) {
            Gson gson = new Gson();
            result = gson.fromJson(json, classOfT);

            if(result != null) {
                DebugMsg("Converted result: " + result.toString());
            }
        }

        return result;
    }

    @Override
    public final <T> T ExecuteAndConvert(Type typeOfT) {
        T result = null;
        String json = Execute();

        if(json != null) {
            Gson gson = new Gson();
            result = gson.fromJson(json, typeOfT);

            if(result != null) {
                DebugMsg("Converted result: " + result.toString());
            }
        }

        return result;
    }

    protected abstract void SetupConnection(HttpURLConnection conn) throws java.io.IOException;
}
