import { lowestFuelCost, totalFuelCostWhenTheTargetPosisitionIs } from "./lowestFuelCost"

describe(`With the well known list of crab positions`, () => {
    const setOfCrabPositions = [0, 1, 10]

    it(`'[${setOfCrabPositions}]' the fuel cost for a target of 0 is 11`, () => {
        expect(totalFuelCostWhenTheTargetPosisitionIs(0, setOfCrabPositions)).toBe(11)
    })

    it(`'[${setOfCrabPositions}]' the fuel cost for a target of 1 is 11`, () => {
        expect(totalFuelCostWhenTheTargetPosisitionIs(1, setOfCrabPositions)).toBe(10)
    })

    it(`'[${setOfCrabPositions}]' the fuel cost for a target of 10 is 11`, () => {
        expect(totalFuelCostWhenTheTargetPosisitionIs(10, setOfCrabPositions)).toBe(19)
    })
})

test("With the input from the webpage", () => {
    expect(totalFuelCostWhenTheTargetPosisitionIs(2, [16,1,2,0,4,2,7,1,2,14])).toBe(37)
})
export { }

describe("finding the lowest fuel usage given the list of positions", () => {
    it("[0, 1, 10] is 10", () => {
        expect(lowestFuelCost([0, 1, 10])).toBe(10)
    })

    it("[16,1,2,0,4,2,7,1,2,14] is 2", () => {
        expect(lowestFuelCost([16,1,2,0,4,2,7,1,2,14])).toBe(37)
    })
})