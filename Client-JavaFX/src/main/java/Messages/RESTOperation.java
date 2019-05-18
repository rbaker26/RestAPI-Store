package Messages;

public interface RESTOperation {

    /**
     * Sets body of the request. Should be in JSON. Is optional.
     * @param jsonBody Body of request. Nothing is sent if made null.
     * @return The updated RESTOperation.
     */
    public RESTOperation Body(String jsonBody);

    /**
     * Sets the request's URI. This must get called before using Execute.
     * @param source The source of the URI. In our case, this will be an HTTP url.
     * @return The updated RESTOperation.
     */
    public RESTOperation URI(String source);

    /**
     * Appends a string to the request's URI.
     *
     * Depending on the implementation, this may throw an illegal state exception if called
     * before calling URI.
     * @param postfix The postfix. Should be something like "orders" and not "/orders/".
     * @return The updated RESTOperation.
     */
    public RESTOperation URIPostfix(String postfix);

    /**
     * Executes the operation.
     * @return The JSON string, if successful; otherwise, null.
     * @throws IllegalStateException if the URI has not been set yet.
     */
    public String Execute();

    public <T> T ExecuteAndConvert(java.lang.Class<T> classOfT);

    public <T> T ExecuteAndConvert(java.lang.reflect.Type typeOfT);
}
