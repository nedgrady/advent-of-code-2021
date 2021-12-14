export function createBucketsFromInitialState(initialState: number[]) {
    return [...Array(9).keys()].map(bucketIndex => initialState.filter(number => number == bucketIndex).length)
}

export function ageOneDay(spawnBuckets: number[]) {
    const [
        oldZeroBucket, oldOneBucket, oldTwoBucket, oldThreeBucket, oldFourBucket, oldFiveBucket, oldSixBucket, oldSevenBucket, oldEightBucket
    ] = spawnBuckets;

    return [
        oldOneBucket,
        oldTwoBucket,
        oldThreeBucket,
        oldFourBucket,
        oldFiveBucket,
        oldSixBucket,
        oldSevenBucket + oldZeroBucket,
        oldEightBucket,
        oldZeroBucket
    ];
}
