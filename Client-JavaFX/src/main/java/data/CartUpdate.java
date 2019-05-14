package data;
import static java.util.Objects.hash;
public class CartUpdate {

    public String Email;
    public ProductUpdate productUpdate;



    public CartUpdate() {
        Email = "";
        productUpdate = new ProductUpdate();
    }
    public CartUpdate(String email, ProductUpdate productUpdate) {
        this.Email = email;
        this.productUpdate = productUpdate;
    }


    @Override
    public int hashCode() {
        return hash(Email,productUpdate);
    }

    @Override
    public boolean equals(Object obj) {
        CartUpdate temp = (CartUpdate)obj;
        return this.Email == temp.Email && this.productUpdate == temp.productUpdate;
    }

    @Override
    public String toString() {
        return "Email:\t" + this.Email + "\tProduct ID:\t" + productUpdate.productId +
                "\tQuantity:\t" + productUpdate.quantityToBeRemoved;
    }
}
