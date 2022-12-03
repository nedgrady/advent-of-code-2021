import Lanternfish from "./Lanternfish"

test("Initially has spawned no children", () => {
    const lanternfishUnderTest = new Lanternfish(1)

    expect(lanternfishUnderTest.children).toHaveLength(0)
})

test.each([1, 2, 3, 4, 5, 6, 7, 8])("A lanternfish with an initial timer of %s does not spawn a child after 1 day", (spawnTimer) => {
    const lanternfishUnderTest = new Lanternfish(spawnTimer)
    lanternfishUnderTest.AgeOneDay()
    expect(lanternfishUnderTest.children).toHaveLength(0)
})

test("A lanternfish with an initial timer of 0 spawns a child after 1 day", () => {
    const lanternfishUnderTest = new Lanternfish(0)

    lanternfishUnderTest.AgeOneDay()

    expect(lanternfishUnderTest.children).toHaveLength(1)
})

test("A lanternfish with an initial timer of 1 spawns a child after 2 days", () => {
    const lanternfishUnderTest = new Lanternfish(1)

    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay()

    expect(lanternfishUnderTest.children).toHaveLength(1)
})

test.each([0, 1, 2, 3, 4, 5, 6, 7, 8])("A lanternfish with an initial timer of %s spawns a child after that many plus one day(s)", (spawnTimer) => {
    const lanternfishUnderTest = new Lanternfish(spawnTimer)

    while(spawnTimer-- >= 0)
        lanternfishUnderTest.AgeOneDay()

    expect(lanternfishUnderTest.children).toHaveLength(1)
})

test("A lanternfish with an initial timer of 8 spawns a child after 9 days then a child then another child after 7 more days", () => {
    const lanternfishUnderTest = new Lanternfish(8)

    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 2
    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 4
    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 6
    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 8
    lanternfishUnderTest.AgeOneDay() // 9

    expect(lanternfishUnderTest.children).toHaveLength(1)

    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 2
    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 4
    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 6
    lanternfishUnderTest.AgeOneDay()

    expect(lanternfishUnderTest.children).toHaveLength(2)
})

test("A lanternfish's child initially has no children", () => {
    const parentLanternfish = new Lanternfish(0)

    parentLanternfish.AgeOneDay()

    const childLanternfishUnderTest = parentLanternfish.children[0]

    expect(childLanternfishUnderTest.children).toHaveLength(0)
})


test("A lanternfish's child takes nine days to spawn a new child", () => {
    const parentLanternfish = new Lanternfish(0)

    parentLanternfish.AgeOneDay()

    const childLanternfishUnderTest = parentLanternfish.children[0]

    expect(childLanternfishUnderTest.children).toHaveLength(0)

    parentLanternfish.AgeOneDay()
    parentLanternfish.AgeOneDay() // 2
    parentLanternfish.AgeOneDay()
    parentLanternfish.AgeOneDay() // 4
    parentLanternfish.AgeOneDay()
    parentLanternfish.AgeOneDay() // 6
    parentLanternfish.AgeOneDay()
    parentLanternfish.AgeOneDay() // 8
    parentLanternfish.AgeOneDay() // 9

    expect(childLanternfishUnderTest.children).toHaveLength(1)

})


test("A lanternfish starting with a timer of 0 has a second child after 10 days", () => {
    const lanternfishUnderTest = new Lanternfish(0)

    lanternfishUnderTest.AgeOneDay()

    expect(lanternfishUnderTest.children).toHaveLength(1)

    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 2
    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 4
    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 6
    lanternfishUnderTest.AgeOneDay()

    expect(lanternfishUnderTest.children).toHaveLength(2)

})

test("A lanternfish starting with a timer of 0 has a child after 1 day, then a child after 7 days. The second child has a child after 9 days.", () => {
    const parentLanternFish = new Lanternfish(0)

    parentLanternFish.AgeOneDay()

    expect(parentLanternFish.children).toHaveLength(1)

    parentLanternFish.AgeOneDay()
    parentLanternFish.AgeOneDay() // 2
    parentLanternFish.AgeOneDay()
    parentLanternFish.AgeOneDay() // 4
    parentLanternFish.AgeOneDay()
    parentLanternFish.AgeOneDay() // 6
    parentLanternFish.AgeOneDay()

    expect(parentLanternFish.children).toHaveLength(2)

    const childLanternfishUnderTest = parentLanternFish.children[1]

    parentLanternFish.AgeOneDay()
    parentLanternFish.AgeOneDay() // 2
    parentLanternFish.AgeOneDay()
    parentLanternFish.AgeOneDay() // 4
    parentLanternFish.AgeOneDay()
    parentLanternFish.AgeOneDay() // 6
    parentLanternFish.AgeOneDay()
    parentLanternFish.AgeOneDay() // 8
    parentLanternFish.AgeOneDay()

    expect(childLanternfishUnderTest.children).toHaveLength(1)

})

test("A new lanternfish's family tree count is 1", () => {
    const lanternfishUnderTest = new Lanternfish(0)

    expect(lanternfishUnderTest.familyTreeCount()).toBe(1)
})

test("A new lanternfish's family tree count is 4 after spawning two chilren and one grandchild", () => {
    const lanternfishUnderTest = new Lanternfish(0)

    expect(lanternfishUnderTest.familyTreeCount()).toBe(1)

    lanternfishUnderTest.AgeOneDay() //First child born
    expect(lanternfishUnderTest.familyTreeCount()).toBe(2)

    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 2
    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 4
    lanternfishUnderTest.AgeOneDay()
    lanternfishUnderTest.AgeOneDay() // 6
    lanternfishUnderTest.AgeOneDay() // Second Child Born
    lanternfishUnderTest.AgeOneDay() // 8
    lanternfishUnderTest.AgeOneDay() // First Grandchild born

    expect(lanternfishUnderTest.familyTreeCount()).toBe(4)
})

export { }