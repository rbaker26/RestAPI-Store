package data;

import static java.util.Objects.hash;

public class ProductUpdate {

    public int productId;
    public int quantityToBeRemoved;

    public ProductUpdate()
    {
        productId = 0;
        quantityToBeRemoved = 0;
    }

    public ProductUpdate(int productId, int quantityToBeRemoved)
    {
        this.productId = productId;
        this.quantityToBeRemoved = quantityToBeRemoved;
    }

    @Override
    public int hashCode() {

        return hash(super.hashCode(), this.productId,this.quantityToBeRemoved);
    }

    @Override
    public boolean equals(Object obj) {
        ProductUpdate temp = (ProductUpdate)obj;
        return this.productId == temp.productId && this.quantityToBeRemoved == temp.quantityToBeRemoved;
    }

    @Override
    public String toString() {
        return "Product ID:\t" + this.productId + "\tPurchaseQuantity:\t" + this.quantityToBeRemoved;
    }
}
