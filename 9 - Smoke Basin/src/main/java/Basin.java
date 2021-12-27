import java.awt.*;
import java.util.Objects;

public class Basin implements Comparable {
    private int size;
    private final int rootHeightValue;
    private final Point rootPosition;

    Basin(int size, int rootHeightValue, Point rootPosition) {
        this.size = size;
        this.rootHeightValue = rootHeightValue;
        this.rootPosition = rootPosition;
    }


    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Basin basin = (Basin) o;
        return size == basin.size && rootHeightValue == basin.rootHeightValue && Objects.equals(rootPosition, basin.rootPosition);
    }

    @Override
    public int hashCode() {
        return Objects.hash(size, rootHeightValue, rootPosition);
    }

    @Override
    public int compareTo(Object other) {
        return Integer.compare(hashCode(), other.hashCode());
    }

    public int getSize() {
        return size;
    }

    @Override
    public String toString() {
        return "Basin{" +
                "size=" + size +
                ", rootHeightValue=" + rootHeightValue +
                ", rootPosition=" + rootPosition +
                '}';
    }
}
