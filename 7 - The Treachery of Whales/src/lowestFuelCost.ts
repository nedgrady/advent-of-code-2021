
export function lowestFuelCost(crabPositions: number[]): number {
    const positionWithLowestFuleCost = median(crabPositions)

    return totalFuelCostWhenTheTargetPosisitionIs(positionWithLowestFuleCost, crabPositions)
}


export function totalFuelCostWhenTheTargetPosisitionIs(targetNumber: number, setOfCrabPositions: number[]): number {
    let runningTotalFuelUsage = 0
    for(const currentStartPosition of setOfCrabPositions)
        runningTotalFuelUsage += Math.abs(targetNumber - currentStartPosition)
    return runningTotalFuelUsage
}


function median(values: number[]) {

    values.sort(function (a, b) {
        return a - b;
    });

    var half = Math.floor(values.length / 2);

    if (values.length % 2)
        return values[half];

    return (values[half - 1] + values[half]) / 2.0;
}