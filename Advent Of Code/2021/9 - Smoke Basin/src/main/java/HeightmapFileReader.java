import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.Arrays;

public class HeightmapFileReader {
    public int[][] ReadFromFile(Path inMemoryPath) throws IOException {
        var lines = Files.readAllLines(inMemoryPath);

        int[][] linesAsNumbers = new int[lines.size()][];

        for (int lineIndex = 0; lineIndex < lines.size(); lineIndex++) {
            linesAsNumbers[lineIndex] =
                    Arrays.stream(lines.get(lineIndex).split(""))
                            .map(Integer::parseInt)
                            .mapToInt(Integer::intValue)
                            .toArray();
        }

        return linesAsNumbers;
    }
}
