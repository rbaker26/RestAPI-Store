package Messages;

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

    protected abstract String AbstractExecute();
}
