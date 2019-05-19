package Messages.RESTobjects;

public final class REST {
    private REST() {}

    public static RESTOperation Get() {
        return new RESTGetOperation();
    }

    /*
    public RESTOperation Get(String serviceName) {
        return Get(serviceName, "");
    }

    public RESTOperation Get(String serviceName, String identifier) {
        String url;

        if(identifier != null && !identifier.equals("")) {
            url = String.join("/", RESTConfig.serverURL, serviceName, identifier);
        }
        else {
            url = String.join("/", RESTConfig.serverURL, serviceName);
        }

        return new RESTGetOperation(url);
    }
     */

}
