package Messages;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import data.Product;

import java.lang.reflect.Type;
import java.util.ArrayList;

public abstract class AbstractRESTOperation implements RESTOperation {

    private String uri;
    private String body;

    public AbstractRESTOperation() {
        uri = null;
        body = null;
    }

    protected String GetURI() { return uri; }
    protected String GetBody() { return body; }

    @Override
    public final RESTOperation Body(String jsonBody) {
        return null;
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
    public final String Execute() {
        if(uri == null) {
            throw new IllegalStateException("Cannot execute REST operation without a URI");
        }

        return AbstractExecute();
    }

    @Override
    public final <T> T ExecuteAndConvert(Class<T> classOfT) {
        T result = null;
        String json = Execute();

        if(json != null) {
            Gson gson = new Gson();
            Type collectionType = new TypeToken<ArrayList<Product>>() {
            }.getType();
            result = gson.fromJson(json, collectionType);
            //ProductUpdate productUpdate = gson.fromJson(jsonStr, ProductUpdate.class);
            System.out.println("**********&&&&&&&&&&&&&&&&&&&&&&&&&&************************");
            System.out.println(result);
            System.out.println("**********************************");
        }

        return result;
    }

    @Override
    public final <T> T ExecuteAndConvert(Type typeOfT) {
        T result = null;
        String json = Execute();

        if(json != null) {
            Gson gson = new Gson();
            Type collectionType = new TypeToken<ArrayList<Product>>() {
            }.getType();
            result = gson.fromJson(json, collectionType);
            //ProductUpdate productUpdate = gson.fromJson(jsonStr, ProductUpdate.class);
            System.out.println("**********&&&&&&&&&&&&&&&&&&&&&&&&&&************************");
            System.out.println(result);
            System.out.println("**********************************");
        }

        return result;
    }

    protected abstract String AbstractExecute();
}
