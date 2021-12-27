import java.awt.*;
import java.util.Objects;

public class HeightPoint implements Comparable{
    public Point position;
    public int height;
    Basin containingBasin;

    public HeightPoint(Point position, int height) {
        this.position = position;
        this.height = height;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        HeightPoint that = (HeightPoint) o;
        return height == that.height && position.equals(that.position);
    }

    @Override
    public int hashCode() {
        return Objects.hash(position, height);
    }

    @Override
    public int compareTo(Object o) {
        return Integer.compare(hashCode(), o.hashCode());
    }
}
