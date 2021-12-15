import { totalSteppedUpFuelCostWhenTheTargetPosisitionIs, steppedUpCostBetweenTwoPositions, bestSteppedUpCostForCrabPositions } from "./totalSteppedUpFuelCostWhenTheTargetPosisitionIs"

/**
Move from 16 to 5: 66 fuel
Move from 1 to 5: 10 fuel
Move from 2 to 5: 6 fuel
Move from 0 to 5: 15 fuel
Move from 4 to 5: 1 fuel
Move from 2 to 5: 6 fuel
Move from 7 to 5: 3 fuel
Move from 1 to 5: 10 fuel
Move from 2 to 5: 6 fuel
Move from 14 to 5: 45 fuel
 */
const costBetweenTwoPositionsCases = [
    [16, 5, 66],
    [1, 5, 10],
    [2, 5, 6],
    [14, 5, 45]
]

test.each(costBetweenTwoPositionsCases)("The stepped up fuel cost between position '%s' and '%s' is '%s'", (pointA, pointB, expectedCost) => {
    expect(steppedUpCostBetweenTwoPositions(pointA, pointB)).toBe(expectedCost)
})
export {}
describe(`With the well known list of crab positions`, () => {
    const setOfCrabPositions = [0, 1, 10]

    it(`'[${setOfCrabPositions}]' the fuel cost for a target of 0 is 56`, () => {
        expect(totalSteppedUpFuelCostWhenTheTargetPosisitionIs(0, setOfCrabPositions)).toBe(56)
    })

    it(`'[${setOfCrabPositions}]' the fuel cost for a target of 1 is 46`, () => {
        expect(totalSteppedUpFuelCostWhenTheTargetPosisitionIs(1, setOfCrabPositions)).toBe(46)
    })

    it(`'[${setOfCrabPositions}]' the fuel cost for a target of 10 is 110`, () => {
        expect(totalSteppedUpFuelCostWhenTheTargetPosisitionIs(10, setOfCrabPositions)).toBe(100)
    })

    it("'[16,1,2,0,4,2,7,1,2,14]' the fuel cost for a target of 5 is 168", () => {
        expect(totalSteppedUpFuelCostWhenTheTargetPosisitionIs(5, [16,1,2,0,4,2,7,1,2,14])).toBe(168)
    })
})

test("Best stepped up cost for [0, 1, 4] is 37", () => {
    expect(bestSteppedUpCostForCrabPositions([0, 1, 4])).toBe(37)
})

test("Best stepped up cost for [16,1,2,0,4,2,7,1,2,14] is 168", () => {
    expect(bestSteppedUpCostForCrabPositions([16,1,2,0,4,2,7,1,2,14])).toBe(168)
})

