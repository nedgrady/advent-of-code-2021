import org.junit.jupiter.api.Test;

import java.awt.*;
import java.util.Arrays;
import java.util.HashSet;
import java.util.Objects;

import static org.junit.jupiter.api.Assertions.assertArrayEquals;

public class FindingBasins {

    @Test
    public void WithASizeOfOne() {

        HashSet<HeightPoint> test = new HashSet<>();

        test.add(new HeightPoint(new Point(1, 1), 1));
        test.add(new HeightPoint(new Point(1, 1), 1));


        var rawHeightMapData = new int[][]{
                {0, 9},
                {9, 9},
        };
        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualBasins = heightMapUnderTest.getBasins();

        var expectedBasin = new Basin(1, 0, new Point(0, 0));
        assertArrayEquals(new Basin[]{expectedBasin}, actualBasins);
    }

    @Test
    public void InTheTopLeft() {

        HashSet<HeightPoint> test = new HashSet<>();

        test.add(new HeightPoint(new Point(1, 1), 1));
        test.add(new HeightPoint(new Point(1, 1), 1));


        var rawHeightMapData = new int[][]{
                {0, 1, 9},
                {1, 1, 9},
                {9, 9, 9},
        };
        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualBasins = heightMapUnderTest.getBasins();

        var expectedBasin = new Basin(4, 0, new Point(0, 0));
        assertArrayEquals(new Basin[]{expectedBasin}, actualBasins);
    }

    @Test
    public void InTheTopRight() {
        var rawHeightMapData = new int[][]{
                {9, 1, 0},
                {9, 9, 9},
                {9, 9, 9},
        };
        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualBasins = heightMapUnderTest.getBasins();

        var expectedBasin = new Basin(2, 0, new Point(2, 0));
        assertArrayEquals(new Basin[]{expectedBasin}, actualBasins);
    }

    @Test
    public void InTheBottomLeft() {
        var rawHeightMapData = new int[][]{
                {8, 9, 9},
                {7, 7, 9},
                {6, 7, 9},
        };
        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualBasins = heightMapUnderTest.getBasins();

        var expectedBasin = new Basin(5, 6, new Point(0, 2));
        assertArrayEquals(new Basin[]{expectedBasin}, actualBasins);
    }

    @Test
    public void InTheBottomRight() {
        var rawHeightMapData = new int[][]{
                {3, 4, 9},
                {2, 2, 2},
                {2, 2, 1},
        };
        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualBasins = heightMapUnderTest.getBasins();

        var expectedBasin = new Basin(8, 1, new Point(2, 2));
        assertArrayEquals(new Basin[]{expectedBasin}, actualBasins);
    }

    @Test
    public void InVariousPositions() {
        var rawHeightMapData = new int[][]{
                {0, 1, 9, 9, 1, 0},
                {1, 1, 9, 9, 9, 9},
                {9, 9, 9, 9, 9, 9},
                {8, 9, 9, 3, 4, 9},
                {7, 7, 9, 2, 2, 2},
                {6, 7, 9, 2, 2, 1},
        };

        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualBasins = heightMapUnderTest.getBasins();

        var topLeftBasin = new Basin(4, 0, new Point(0, 0));
        var topRightBasin = new Basin(2, 0, new Point(5, 0));
        var bottomLeftBasin = new Basin(5, 6, new Point(0, 5));
        var bottomRightBasin = new Basin(8, 1, new Point(5, 5));
        var expectedBasins = new Basin[]{
                topLeftBasin,
                topRightBasin,
                bottomLeftBasin,
                bottomRightBasin
        };

        assertArrayEquals(Arrays.stream(expectedBasins).sorted().toArray(), Arrays.stream(actualBasins).sorted().toArray());
    }
}

