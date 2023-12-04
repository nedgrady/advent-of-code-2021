require 'humanize'

class GameChecker
  def initialize(game_configuration_hash)
    @game_configuration_hash = game_configuration_hash
  end

  def is_game_possible?(game_record)
    color_counts = {}
    @game_configuration_hash.each_pair do |color, count|
      find_the_color_regex = /\d+ #{color}/
      max_for_color = game_record.scan(find_the_color_regex).map { |color_count| color_count.split(' ')[0].to_i }.max

      color_counts[color] = max_for_color
    end

    !color_counts.any? { |color, value| value > @game_configuration_hash[color] }
  end

end



if __FILE__ == $0
  game_configuration_hash = {
    red: 12,
    green: 13,
    blue: 14
  }

  game_checker = GameChecker.new game_configuration_hash
  running_sum_of_ids = 0
  (File.readlines './input.txt').each do |line|
    game_id = line.match(/Game (\d+):/)[1]

    running_sum_of_ids += game_id.to_i if game_checker.is_game_possible?(line)
  end
  puts running_sum_of_ids
end