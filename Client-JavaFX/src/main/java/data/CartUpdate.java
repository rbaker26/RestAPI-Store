package data;
import static java.util.Objects.hash;
public class CartUpdate {

    public String email;
    public ProductUpdate productUpdate;



    public CartUpdate() {
        email = "";
        productUpdate = new ProductUpdate();
    }
    public CartUpdate(String email, ProductUpdate productUpdate) {
        this.email = email;
        this.productUpdate = productUpdate;
    }


    @Override
    public int hashCode() {
        return hash(email,productUpdate);
    }

    @Override
    public boolean equals(Object obj) {
        CartUpdate temp = (CartUpdate)obj;
        return this.email == temp.email && this.productUpdate == temp.productUpdate;
    }

    @Override
    public String toString() {
        return "Email:\t" + this.email + "\tProduct ID:\t" + productUpdate.productId +
                "\tQuantity:\t" + productUpdate.quantityToBeRemoved;
    }
}
