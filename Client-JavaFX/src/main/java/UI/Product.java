package UI;

public class Product {

    /******************
     * Data Properties
     ******************/

    public int ProductId;
    public String Description;
    public int Quantity;
    public float Price;

    public int getProductId() {
        return ProductId;
    }

    public String getDescription() {
        return Description;
    }

    public int getQuantity() {
        return Quantity;
    }

    public float getPrice() {
        return Price;
    }

    public void setProductId(int productId) {
        ProductId = productId;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public void setQuantity(int quantity) {
        Quantity = quantity;
    }

    public void setPrice(float price) {
        Price = price;
    }

    /***************
     * Constructors
     ***************/

    public Product(int ProductId, String Description, int Quantity, float Price) {
        this.ProductId = ProductId;
        this.Description = Description;
        this.Quantity = Quantity;
        this.Price = Price;
    }

    public Product() {
        this.ProductId = 0;
        this.Description = null;
        this.Quantity = 0;
        this.Price = 0;
    }

    @Override
    public String toString() {
        return "Product{" +
                "ProductId=" + ProductId +
                ", Description='" + Description + '\'' +
                ", Quantity=" + Quantity +
                ", Price=" + Price +
                '}';
    }
}

