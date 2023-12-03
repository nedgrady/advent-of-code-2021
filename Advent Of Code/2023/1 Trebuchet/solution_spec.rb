require_relative 'solution'

test_cases = [
  { calibration_line: '1abc2', expected_calibration_value: 12 },
  { calibration_line: 'pqr3stu8vwx', expected_calibration_value: 38 },
  { calibration_line: 'a1b2c3d4e5f', expected_calibration_value: 15 },
  { calibration_line: 'treb7uchet', expected_calibration_value: 77 },
  { calibration_line: 'two1nine', expected_calibration_value: 29 },
  { calibration_line: 'eightwothree', expected_calibration_value: 83 },
  { calibration_line: 'abcone2threexyz', expected_calibration_value: 13 },
  { calibration_line: 'xtwone3four', expected_calibration_value: 24 },
  { calibration_line: '4nineeightseven2', expected_calibration_value: 42 },
  { calibration_line: 'zoneight234', expected_calibration_value: 14 },
  { calibration_line: '7pqrstsixteen', expected_calibration_value: 76 },
]

RSpec.describe 'get_calibration_value' do
  test_cases.each do |test_case|
    it "returns the number of integers in #{test_case[:calibration_line]}" do
      get_calibration_value(test_case[:calibration_line]).should eq(test_case[:expected_calibration_value])
    end
  end
end
