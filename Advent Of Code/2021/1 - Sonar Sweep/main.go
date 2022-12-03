package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"strconv"
)

func main() {
	inputFile, _ := os.Open("input.txt")
	defer inputFile.Close()

	scanner := bufio.NewScanner(inputFile)
	scanner.Scan()
	var threeMeasurementsPrevious, _ = strconv.Atoi(scanner.Text())
	scanner.Scan()
	var twoMeasurementsPrevious, _ = strconv.Atoi(scanner.Text())
	var currentMeasurement int
	var previousWindowSum = math.MaxInt32
	var countOfIncreasingWindows = 0

	for scanner.Scan() {
		currentMeasurement, _ = strconv.Atoi(scanner.Text())
		currentWindowSum := threeMeasurementsPrevious + twoMeasurementsPrevious + currentMeasurement

		if currentWindowSum > previousWindowSum {
			countOfIncreasingWindows++
		}

		threeMeasurementsPrevious = twoMeasurementsPrevious
		twoMeasurementsPrevious = currentMeasurement
		previousWindowSum = currentWindowSum

	}

	fmt.Printf("Total increasing windows: %d", countOfIncreasingWindows)
}
