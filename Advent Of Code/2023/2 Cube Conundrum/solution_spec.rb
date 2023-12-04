require_relative 'solution'

test_cases = [
  { game_record: 'Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green', game_possible: true },
  { game_record: 'Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue', game_possible: true },
  { game_record: 'Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red', game_possible: false },
  { game_record: 'Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red', game_possible: false },
  { game_record: 'Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green', game_possible: true }
]

RSpec.describe 'solution' do
  test_cases.each do |test_case|
    it "Returns #{test_case[:game_possible]} #{test_case[:game_record]}" do
      game_configuration_hash = {
        red: 12,
        green: 13,
        blue: 14
      }
      game_checker = GameChecker.new game_configuration_hash

      expect(game_checker.is_game_possible?(test_case[:game_record])).to eq(test_case[:game_possible])
    end
  end
end
