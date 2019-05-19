package data;

import javafx.beans.property.*;

public class PurchaseInfo {

    private Product product;
    private final StringProperty description;
    private final IntegerProperty qty;
    private final FloatProperty price;

    public PurchaseInfo(Product product, int qty) {
        this.qty = new SimpleIntegerProperty(qty);
        this.description = new SimpleStringProperty(product.description);
        this.price = new SimpleFloatProperty(product.price);
        this.product = product;
    }

    public String getDescription() {
        return description.get();
    }

    public StringProperty descriptionProperty() {
        return description;
    }

    public int getQty() {
        return qty.get();
    }

    public IntegerProperty qtyProperty() {
        return qty;
    }

    public float getPrice() {
        return price.get();
    }

    public FloatProperty priceProperty() {
        return price;
    }
    public Product getProduct() {
        return product;
    }

    public Integer getProductId() {
        return product.productId;
    }
}
