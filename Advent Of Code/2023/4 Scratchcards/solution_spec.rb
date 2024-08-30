require_relative 'solution'



test_cases = [
  { raw_value: 'Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53', expected_points: 8 },
  { raw_value: 'Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19', expected_points: 2 },
  { raw_value: 'Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1', expected_points: 2 },
  { raw_value: 'Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83', expected_points: 1 },
  { raw_value: 'Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36', expected_points: 0 },
  { raw_value: 'Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11', expected_points: 0 },
]



RSpec.describe 'solution' do
  test_cases.each do |test_case|
    it 'Part 1' do
      scratchcard = Scratchcard.new(test_case[:raw_value])

      expect(scratchcard.points).to eq(test_case[:expected_points])
    end
  end

  it 'Card number' do
    scratchcard = Scratchcard.new('Card 180: 18 98 61 58 87 41 51 37 28 79 | 65 32 36 92 49 45 10 93 40  4 67 76 55 42 88 30 75 44 23 71 98 51 14 78 24')

    expect(scratchcard.card_number).to eq(180)
  end

  it 'part 2'  do
    all_the_cards = test_cases.map { |test_case| Scratchcard.new(test_case[:raw_value]) }

    scratchcard_cloner = ScratchcardCloner.new(all_the_cards)

    expect(scratchcard_cloner.count).to eq(30)
  end

  it 'Part 2 with one card' do 
    scratcard = Scratchcard.new('Card 1: 31 18 13 56 72 | 74 77 10 23 35 67 36 11')

    scratchcard_cloner = ScratchcardCloner.new([scratcard])
    expect(scratchcard_cloner.count).to eq(1)
  end

  it 'Part 2 with 3 cards' do

    all_the_cards = [
      Scratchcard.new('Card 1: 41 92 73 84 69 | 59 84 76 51 58  5 54 83'),
      Scratchcard.new('Card 2: 87 83 26 28 32 | 88 30 70 12 93 22 82 36'),
      Scratchcard.new('Card 3: 31 18 13 56 72 | 74 77 10 23 35 67 36 11')
    ]

    scratchcard_cloner = ScratchcardCloner.new(all_the_cards)

    expect(scratchcard_cloner.count).to eq(4)
  end

  it 'part 2 with many cards' do
    all_the_cards = [
      Scratchcard.new('1: 34 50 18 44 19 35 47 62 65 26 | 63  6 27 15 60  9 98  3 61 89 31 43 80 37 54 49 92 55  8  7 10 16 52 33 45'),
      Scratchcard.new('2: 90 12 98 56 22 99 73 46  1 28 | 52 77 32  8 81 41 53 22 28 46 48 27 98  1 94 12 99 72 84 90 92 73 24 63 56'),
      Scratchcard.new('3: 48 10 39 87 23 78 49 40 55  8 | 48 80 78 87 39 24 27 19 41 73 30 52 10  2 67 40 88 53 59 84 55 49  5 33 82'),
      Scratchcard.new('4: 21 45 91 26 64 51 42 84 11 94 | 55 56 36 65 84  2 68 44 52 58 86  6 33  7 97 40 30 14 39 80 82 57 79  1 10'),
      Scratchcard.new('5: 33  6 67 89 64 31 85 11  2 15 |  6 70 29 89 12 11 64 80  7 82 46 16 33 68 48 72 31  2 99 15 67 57  4 49 85'),
      Scratchcard.new('6: 51 20 11 66 38 39 69 48 25 74 | 39 74  3 86 19 25 21 55  2 38 46 60 66 82 51 11 98 88  8 48 49 94 20 69 72'),
      Scratchcard.new('7:  4 50 82 51 52 77 12 11 57 42 | 56 11 73 69 42 82 32 77 52 98 12 51 36 94 46  4 50 39 85 90 93 70 18 71 57'),
      Scratchcard.new('8: 96 31 27 93  7  8  6 23 15 72 | 55 79 86  4  6 35 12 27 95 29 73 81 87 43  7 13 62 15 72 71 58 48 63 94 89'),
      Scratchcard.new('9: 16 90 79 29 93 31 40 24 82 88 | 86 16 73 20 22 93 83 39 36 90 79 72 40 29 35 97 88 12  8 24 31 82 21 59 95')
    ]

    
    scratchcard_cloner = ScratchcardCloner.new(all_the_cards)

    expect(scratchcard_cloner.count).to eq(4)
  end
end 
