import java.util.ArrayList;

public class HeightMap {
    private int[][] rawHeightMapData;


    public HeightMap(int[][] rawHeightMapData) {
        this.rawHeightMapData = rawHeightMapData;
    }

    public int[] getLowPoints() {
        ArrayList<Integer> foundLowPoints = new ArrayList<Integer>();
        for (int i = 0; i < rawHeightMapData.length; i++) {
            for (int j = 0; j < rawHeightMapData[i].length; j++) {
                var potentialLowPoint = getHeightAtPosition(i, j);
                if((potentialLowPoint < getHeightAtPosition(i, j - 1))
                        && (potentialLowPoint < getHeightAtPosition(i, j + 1))
                        && (potentialLowPoint < getHeightAtPosition(i - 1, j))
                        && (potentialLowPoint < getHeightAtPosition(i + 1, j))){
                    foundLowPoints.add(potentialLowPoint);
                }
            }
        }

        return foundLowPoints.stream().mapToInt(i -> i).toArray();
    }

    private int getHeightAtPosition(int xPosition, int yPosition){
        try {
            return rawHeightMapData[xPosition][yPosition];
        } catch (IndexOutOfBoundsException e) {
            return Integer.MAX_VALUE;
        }
    }
}


