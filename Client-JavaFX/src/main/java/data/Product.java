package data;
import static java.util.Objects.hash;
public class Product {

    public int ProductId ;

    public String Description ;

    public int Quantity ;

    public float Price ;


    public Product() {
        ProductId = 0;
        Description="";
        Quantity = 0;
        Price = 0;
    }

    public Product(int productId, String description, int quantity, float price) {
        ProductId = productId;
        Description = description;
        Quantity = quantity;
        Price = price;
    }

    @Override
    public int hashCode() {
        return hash(ProductId,Description,Quantity,Price);
    }

    @Override
    public boolean equals(Object obj) {

        Product temp = (Product)obj;
        return this.ProductId == temp.ProductId && this.Description.equals(temp.Description) &&
               this.Quantity == temp.Quantity && this.Price == temp.Price;
    }

    @Override
    public String toString() {
        return "Product ID:\t" + ProductId + "\tDescription:\t" + Description +
               "\tQuantity:\t" + Quantity + "\tPrice:\t" + Price;
    }
}
