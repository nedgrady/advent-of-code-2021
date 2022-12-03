import java.awt.*;
import java.io.IOException;
import java.nio.file.FileSystems;
import java.util.Arrays;
import java.util.Collections;
import java.util.Comparator;

public class main {
    public static void main (String[] args) throws IOException {
        HeightmapFileReader heightmapFileReader = new HeightmapFileReader();
        var inputTxtPath = FileSystems.getDefault().getPath("C:", "code", "advent-of-code-2021", "9 - Smoke Basin", "input.txt");
        heightmapFileReader.ReadFromFile(inputTxtPath);

        var heightMap = new HeightMap(heightmapFileReader.ReadFromFile(inputTxtPath));

        var lowPoints = heightMap.getLowPoints();
        var sumOfLowPointDangerLevels = Arrays.stream(lowPoints).mapToInt(lowPoint -> lowPoint.height).sum() + lowPoints.length;
        System.out.println("Sum of low point danger levels is: " + sumOfLowPointDangerLevels);

        var basinSizes = Arrays.stream(heightMap.getBasins())
                .mapToInt(basin -> basin.getSize())
                .boxed()
                .sorted(Collections.reverseOrder())
                .mapToInt(i -> i)
                .toArray();

        System.out.println("Product of top three basin sizes is: " + basinSizes[0] * basinSizes[1] * basinSizes[2]);
    }
}
