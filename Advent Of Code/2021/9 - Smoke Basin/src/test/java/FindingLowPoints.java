import com.google.common.jimfs.Jimfs;
import org.junit.jupiter.api.Test;
import com.google.common.jimfs.*;

import javax.naming.Name;
import java.awt.*;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.*;
import java.util.Arrays;
import java.util.Random;

import static org.junit.jupiter.api.Assertions.*;

public class FindingLowPoints {


    @Test
    public void InTheTopLeftCorner() {
        var rawHeightMapData = new int[][]{
                {1, 2},
                {2, 3}
        };
        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualLowPoints = heightMapUnderTest.getLowPoints();
        var expectedLowPoints = new HeightPoint[] { new HeightPoint(new Point(0, 0), 1) };

        assertArrayEquals(expectedLowPoints, actualLowPoints);
    }

    @Test
    public void InTheTopRightCorner() {
        var rawHeightMapData = new int[][]{
                {9, 0},
                {9, 9}
        };
        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualLowPoints = heightMapUnderTest.getLowPoints();
        var expectedLowPoints = new HeightPoint[] { new HeightPoint(new Point(1, 0), 0) };
        assertArrayEquals(expectedLowPoints, actualLowPoints);
    }

    @Test
    public void InTheBottomLeftCorner() {
        var rawHeightMapData = new int[][]{
                {8, 8, 8, 8},
                {6, 8, 8, 8}
        };
        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualLowPoints = heightMapUnderTest.getLowPoints();
        var expectedLowPoints = new HeightPoint[] { new HeightPoint(new Point(0, 1), 6) };
        assertArrayEquals(expectedLowPoints, actualLowPoints);
    }

    @Test
    public void InTheBottomRightCorner() {
        var rawHeightMapData = new int[][]{
                {1012, 1007, 1002, 1000},
                {1007, 1001, 1000, 10}
        };
        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualLowPoints = heightMapUnderTest.getLowPoints();
        var expectedLowPoints = new HeightPoint[] { new HeightPoint(new Point(3, 1), 10) };
        assertArrayEquals(expectedLowPoints, actualLowPoints);
    }

    @Test
    public void InVariousPlaces() {
        var rawHeightMapData = new int[][]{
                {1012, 1007, 1002, 1000, 1111},
                {1007, 1001, 1000, 10, 1111},
                {1007, 1, 1000, 1111, 1111},
                {1007, 1001, 1000, 1111, 1111},
                {9, 1001, 1000, 1111, 2}
        };
        HeightMap heightMapUnderTest = new HeightMap(rawHeightMapData);

        var actualLowPoints = Arrays.stream(heightMapUnderTest.getLowPoints()).sorted().toArray();
        var expectedLowPoints = new HeightPoint[] {
                new HeightPoint(new Point(3, 1), 10),
                new HeightPoint(new Point(1, 2), 1),
                new HeightPoint(new Point(0, 4), 9),
                new HeightPoint(new Point(4, 4), 2)
        };


        assertArrayEquals(Arrays.stream(expectedLowPoints).sorted().toArray(), actualLowPoints);
    }
}