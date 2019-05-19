package data;

public class PurchaseInfo {

    private Product product;
    private int qty;
    public PurchaseInfo(Product product, int qty) {

        this.product = product;
        this.qty = qty;
    }

    public Product getProduct() {
        return product;
    }

    public int getQty() {
        return qty;
    }

    public int getProductId() {
        return product.productId;
    }

    public String getDescription() {
        return product.description;
    }

    public float getPrice() {
        return product.price;
    }
}
