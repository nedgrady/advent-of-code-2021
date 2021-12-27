import java.awt.*;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Set;
import java.util.stream.Stream;

public class HeightMap {
    private int[][] rawHeightMapData;
    private HeightPoint[][] heightPoints;

    final HeightPoint WallPoint = new HeightPoint(new Point(Integer.MAX_VALUE, Integer.MAX_VALUE), 9);
    public HeightMap(int[][] rawHeightMapData) {
        this.rawHeightMapData = rawHeightMapData;
        this.heightPoints = getHeightPoints();
    }

    public HeightPoint[] getLowPoints() {
        ArrayList<HeightPoint> foundLowPoints = new ArrayList<>();
        for (int yPosition = 0; yPosition < rawHeightMapData.length; yPosition++) {
            for (int xPosition = 0; xPosition < rawHeightMapData[yPosition].length; xPosition++) {
                var potentialLowPoint = getHeightAtPosition(xPosition, yPosition);
                if ((potentialLowPoint < getHeightAtPosition(xPosition, yPosition - 1))
                        && (potentialLowPoint < getHeightAtPosition(xPosition, yPosition + 1))
                        && (potentialLowPoint < getHeightAtPosition(xPosition - 1, yPosition))
                        && (potentialLowPoint < getHeightAtPosition(xPosition + 1, yPosition))) {
                    foundLowPoints.add(new HeightPoint(new Point(xPosition, yPosition), potentialLowPoint));
                }
            }
        }
        return foundLowPoints.toArray(new HeightPoint[0]);
    }

    public Basin[] getBasins() {
        var heightPoints = getHeightPoints();
        var rootLowPoints = getLowPoints();
        var fullyExploredBasins = new ArrayList<Basin>();

        for (var currentLowPoint : rootLowPoints) {
            HashSet<HeightPoint> toExplore = new HashSet<>();
            HashSet<HeightPoint> alreadyExplored = new HashSet<>();

            toExplore.add(currentLowPoint);
            HeightPoint currentPointBeingExplored;
            while(!toExplore.isEmpty()){
                currentPointBeingExplored = toExplore.stream().findFirst().get();

                var potentialNextPointsToExplore = AdjacentHeightPointsNotAtAWall(currentPointBeingExplored);
                var unexploredNextPoints = potentialNextPointsToExplore.stream().filter(heightPoint -> !alreadyExplored.contains(heightPoint)).toList();

                toExplore.addAll(unexploredNextPoints);
                alreadyExplored.add(currentPointBeingExplored);
                toExplore.remove(currentPointBeingExplored);
            }

            fullyExploredBasins.add(new Basin(alreadyExplored.size(), currentLowPoint.height, currentLowPoint.position));
        }

        return fullyExploredBasins.toArray(new Basin[0]);
    }

    private HeightPoint Left(HeightPoint basePoint) {
        return getHeightPointAtPosition(basePoint.position.x - 1, basePoint.position.y);
    }

    private HeightPoint Right(HeightPoint basePoint) {
        return getHeightPointAtPosition(basePoint.position.x + 1, basePoint.position.y);
    }

    private HeightPoint Up(HeightPoint basePoint) {
        return getHeightPointAtPosition(basePoint.position.x, basePoint.position.y - 1);
    }

    private HeightPoint Down(HeightPoint basePoint) {
        return getHeightPointAtPosition(basePoint.position.x, basePoint.position.y + 1);
    }

    private ArrayList<HeightPoint> AdjacentHeightPointsNotAtAWall(HeightPoint basePoint) {
        var pointsToReturn = new ArrayList<HeightPoint>();

        var left = Left(basePoint);
        var right = Right(basePoint);
        var up = Up(basePoint);
        var down = Down(basePoint);

        if(left != WallPoint && left.height < 9) pointsToReturn.add(left);
        if(Right(basePoint) != WallPoint && Right(basePoint).height < 9) pointsToReturn.add(Right(basePoint));
        if(Up(basePoint) != WallPoint && Up(basePoint).height < 9) pointsToReturn.add(Up(basePoint));
        if(Down(basePoint) != WallPoint && Down(basePoint).height < 9) pointsToReturn.add(Down(basePoint));

        return pointsToReturn;
    }

    private HeightPoint[][] getHeightPoints() {
        var heightPoints = new HeightPoint[rawHeightMapData.length][rawHeightMapData[0].length];
        for (int yPosition = 0; yPosition < heightPoints.length; yPosition++) {
            for (int xPosition = 0; xPosition < heightPoints[yPosition].length; xPosition++) {
                var heightPoint = new HeightPoint(new Point(xPosition, yPosition), rawHeightMapData[yPosition][xPosition]);
                heightPoints[yPosition][xPosition] = heightPoint;
            }
        }
        return heightPoints;
    }


    private int getHeightAtPosition(int xPosition, int yPosition) {
        try {
            return rawHeightMapData[yPosition][xPosition];
        } catch (IndexOutOfBoundsException e) {
            return Integer.MAX_VALUE;
        }
    }

    private HeightPoint getHeightPointAtPosition(int xPosition, int yPosition) {
        try {
            return heightPoints[yPosition][xPosition];
        } catch (IndexOutOfBoundsException e) {
            return WallPoint;
        }
    }
}

