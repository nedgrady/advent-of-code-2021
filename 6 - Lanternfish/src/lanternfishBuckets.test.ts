import { createBucketsFromInitialState , ageOneDay } from "./lanternfishBuckets"

test("Map gets initialised properly v1", () => {
    const initialState = [0, 1, 2, 3, 4, 5, 6, 7, 8]

    const spawnTracker = createBucketsFromInitialState(initialState)

    expect(spawnTracker).toEqual([
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1
    ])
})

test("Map gets initialised properly v2", () => {
    const initialState = [0, 0, 1, 2, 3, 4, 4, 5, 6, 7, 8, 8, 8, 8]

    const spawnTracker = createBucketsFromInitialState(initialState)

    expect(spawnTracker).toEqual([
        2,
        1,
        1,
        1,
        2,
        1,
        1,
        1,
        4
    ])
})

test("Map gets initialised properly v3", () => {
    const initialState = [1, 8]

    const spawnTracker = createBucketsFromInitialState(initialState)

    expect(spawnTracker).toEqual([
        0,
        1,
        0,
        0,
        0,
        0,
        0,
        0,
        1
    ])
})



test("Aging one day shifts all the buckets left one, except the 0 bucket.", () => {
    const initialState = [1, 2, 3, 4, 5, 6, 6, 7, 8]

    const spawnBuckets = createBucketsFromInitialState(initialState)

    const nextDaysSpawnBucketsUnderTest = ageOneDay(spawnBuckets)

    const [_, ...everythingButZeroBucket] = nextDaysSpawnBucketsUnderTest

    expect(everythingButZeroBucket).toEqual([
        1, // 1
        1, 
        1, //3
        1,
        2, // 5
        1,
        1,
        0
    ])
})

test("Aging one day shifts the zero bucket to the 6 bucket, then adds one to the 8 bucket", () => {
    const spawnBuckets      = [1, 1, 1, 1, 1, 1, 1, 1, 1]
    const expectedBuckets   = [1, 1, 1, 1, 1, 1, 2, 1, 1]

    const nextDaysSpawnBucketsUnderTest = ageOneDay(spawnBuckets)

    expect(nextDaysSpawnBucketsUnderTest).toEqual(expectedBuckets)
})


