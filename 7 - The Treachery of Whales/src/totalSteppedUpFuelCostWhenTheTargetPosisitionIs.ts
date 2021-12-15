

export function totalSteppedUpFuelCostWhenTheTargetPosisitionIs(targetPosition: number, setOfCrabPositions: number[]): number {
    let runningTotalFuelCost = 0;
    for (const currentPosition of setOfCrabPositions) {
        runningTotalFuelCost += steppedUpCostBetweenTwoPositions(currentPosition, targetPosition);
    }

    return runningTotalFuelCost;
}

export function bestSteppedUpCostForCrabPositions(setOfCrabPositions: number[]) : number {
    let lowestFuelCostSoFar = Infinity
    for (let currentPosition = 0; currentPosition < 50000; currentPosition++) {
        const totalFuelRequiredForAllCrabsToGetToCurrentPosition = totalSteppedUpFuelCostWhenTheTargetPosisitionIs(currentPosition, setOfCrabPositions)
        if(totalFuelRequiredForAllCrabsToGetToCurrentPosition < lowestFuelCostSoFar) {
            //console.log(lowestFuelCostSoFar + " -> " + totalFuelRequiredForAllCrabsToGetToCurrentPosition + " @ " + currentPosition)
            lowestFuelCostSoFar = totalFuelRequiredForAllCrabsToGetToCurrentPosition
        }
    }

    return lowestFuelCostSoFar
}


export function steppedUpCostBetweenTwoPositions(pointA: number, pointB: number): any {
    // (D * (1 + D) / 2)
    const distanceBetweenPoints = Math.abs(pointA - pointB)
    return distanceBetweenPoints * (1 + distanceBetweenPoints) / 2
}
