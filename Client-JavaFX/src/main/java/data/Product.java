package data;
import static java.util.Objects.hash;
public class Product {

    public int productId;

    public String description ;

    public int quantity ;

    public float price ;


    public Product() {
        productId = 0;
        description="";
        quantity = 0;
        price = 0;
    }

    public Product(int productId, String description, int quantity, float price) {
        this.productId = productId;
        this.description = description;
        this.quantity = quantity;
        this.price = price;
    }

    @Override
    public int hashCode() {
        return hash(productId,description,quantity,price);
    }

    @Override
    public boolean equals(Object obj) {

        Product temp = (Product)obj;
        return this.productId == temp.productId && this.description.equals(temp.description) &&
               this.quantity == temp.quantity && this.price == temp.price;
    }

    @Override
    public String toString() {
        return "Product ID:\t" + productId + "\tDescription:\t" + description +
               "\tQuantity:\t" + quantity + "\tPrice:\t" + price;
    }
}
