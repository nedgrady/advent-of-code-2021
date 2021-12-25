import java.io.IOException;
import java.nio.file.FileSystems;
import java.util.Arrays;

public class main {
    public static void main (String[] args) throws IOException {
        HeightmapFileReader heightmapFileReader = new HeightmapFileReader();
        var inputTxtPath = FileSystems.getDefault().getPath("C:", "code", "advent-of-code-2021", "9 - Smoke Basin", "input.txt");
        heightmapFileReader.ReadFromFile(inputTxtPath);

        var heightMap = new HeightMap(heightmapFileReader.ReadFromFile(inputTxtPath));

        var lowPoints = heightMap.getLowPoints();
        var sumOfLowPointDangerLevels = Arrays.stream(lowPoints).sum() + lowPoints.length;
        System.out.println("Sum of low point danger levels is " + sumOfLowPointDangerLevels);

    }
}
