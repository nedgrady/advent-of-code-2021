
class Scratchcard
  def initialize(raw_data)
    @raw_data = raw_data
  end

  def winning_numbers
    @raw_data.split(/\||\:/)[1].scan(/\d+/).map(&:to_i)
  end

  def owned_numbers
    @raw_data.split(/\||\:/)[2].scan(/\d+/).map(&:to_i)
  end

  def points
    winning_number_count = winning_numbers.intersection(owned_numbers).length
    (2 ** (winning_number_count - 1)).floor
  end
end

if __FILE__ == $0
  sum_of_points = 0
  (File.readlines './input.txt').each do |line|
    scratcard = Scratchcard.new(line.chomp)

    sum_of_points += scratcard.points
  end
  puts sum_of_points

end