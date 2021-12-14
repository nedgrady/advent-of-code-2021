export default class Lanternfish {
    #initialSpawnTimer: number

    children: Lanternfish[] = []

    constructor(initialSpawnTimer : number) {
        this.#initialSpawnTimer = initialSpawnTimer
    }

    AgeOneDay() {
        this.children.forEach(child => child.AgeOneDay())
        if(this.#initialSpawnTimer === 0) {
            this.children.push(new Lanternfish(8))
            this.#initialSpawnTimer = 6
        }
        else {
            this.#initialSpawnTimer--
        }
    }

    familyTreeCount() {
        let treeCount = 1;
        for(const child of this.children)
            treeCount+=child.familyTreeCount();
        return treeCount;
    }
}