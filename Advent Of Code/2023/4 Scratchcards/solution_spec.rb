require_relative 'solution'

test_cases = [
  { raw_value: 'Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53', expected_points: 8 },
  { raw_value: 'Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19', expected_points: 2 },
  { raw_value: 'Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1', expected_points: 2 },
  { raw_value: 'Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83', expected_points: 1 },
  { raw_value: 'Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36', expected_points: 0 },
  { raw_value: 'Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11', expected_points: 0 },
  { raw_value: 'Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11', expected_points: 0 },
  { raw_value: 'Card 180: 18 98 61 58 87 41 51 37 28 79 | 65 32 36 92 49 45 10 93 40  4 67 76 55 42 88 30 75 44 23 71 98 51 14 78 24', expected_points: 2 }
]



RSpec.describe 'solution' do
  test_cases.each do |test_case|
    it 'Does a thing' do
      scratchcard = Scratchcard.new(test_case[:raw_value])

      expect(scratchcard.points).to eq(test_case[:expected_points])
    end
  end
end 
