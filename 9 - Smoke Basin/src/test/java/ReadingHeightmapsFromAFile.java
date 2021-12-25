import com.google.common.jimfs.Jimfs;
import org.junit.jupiter.api.Test;
import com.google.common.jimfs.*;

import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.*;

import static org.junit.jupiter.api.Assertions.*;

public class ReadingHeightmapsFromAFile {


    @Test
    public void WorksWithSomeSimpleData() throws IOException {
        FileSystem fileSystem = Jimfs.newFileSystem(Configuration.forCurrentPlatform());

        Path inMemoryPath = fileSystem.getPath("input.txt");

        Files.write(inMemoryPath, "12345\n".getBytes(StandardCharsets.UTF_8), StandardOpenOption.CREATE);
        Files.write(inMemoryPath, "54321\n".getBytes(StandardCharsets.UTF_8), StandardOpenOption.APPEND);
        Files.write(inMemoryPath, "24680\n".getBytes(StandardCharsets.UTF_8), StandardOpenOption.APPEND);
        Files.write(inMemoryPath, "13579\n".getBytes(StandardCharsets.UTF_8), StandardOpenOption.APPEND);

        HeightmapFileReader heightmapFileReaderUnderTest = new HeightmapFileReader();

        int[][] actualHeights = heightmapFileReaderUnderTest.ReadFromFile(inMemoryPath);

        assertArrayEquals(new int[]{1, 2, 3, 4, 5}, actualHeights[0]);
        assertArrayEquals(new int[]{5, 4, 3, 2, 1}, actualHeights[1]);
        assertArrayEquals(new int[]{2, 4, 6, 8, 0}, actualHeights[2]);
        assertArrayEquals(new int[]{1, 3, 5, 7, 9}, actualHeights[3]);
    }


    @Test
    public void AlsoWorksWithThisData() throws IOException {
        FileSystem fileSystem = Jimfs.newFileSystem(Configuration.forCurrentPlatform());

        Path inMemoryPath = fileSystem.getPath("input.txt");

        Files.write(inMemoryPath, "111\n".getBytes(StandardCharsets.UTF_8), StandardOpenOption.CREATE);
        Files.write(inMemoryPath, "222\n".getBytes(StandardCharsets.UTF_8), StandardOpenOption.APPEND);
        Files.write(inMemoryPath, "333\n".getBytes(StandardCharsets.UTF_8), StandardOpenOption.APPEND);

        HeightmapFileReader heightmapFileReaderUnderTest = new HeightmapFileReader();

        int[][] actualHeights = heightmapFileReaderUnderTest.ReadFromFile(inMemoryPath);

        assertArrayEquals(new int[]{1, 1, 1}, actualHeights[0]);
        assertArrayEquals(new int[]{2, 2, 2}, actualHeights[1]);
        assertArrayEquals(new int[]{3, 3, 3}, actualHeights[2]);
    }
}


