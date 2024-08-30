
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

  def matching_numbers
    winning_numbers.intersection(owned_numbers)
  end

  def points
    winning_number_count = matching_numbers.length
    (2 ** (winning_number_count - 1)).floor
  end

  def card_number
    @raw_data.gsub(/\s+/, "").scan(/Card(\d+)/)[0][0].to_i
  end
end

class ScratchcardCloner
  def initialize(all_the_cards)
    @all_the_cards = all_the_cards.clone
    @cards_to_process = all_the_cards.clone
  end

  def count
    current_card_index = 0
    while current_card_index < @cards_to_process.length
      current_card = @cards_to_process[current_card_index]
      puts "Processing card num #{current_card.card_number} idx #{current_card_index} of #{@cards_to_process.length}"

      cloned_cards = @all_the_cards[current_card.card_number.. current_card.card_number + current_card.matching_numbers.length - 1]

      @cards_to_process.concat(cloned_cards)
      current_card_index += 1
    end

    @cards_to_process.length
  end
end


if __FILE__ == $0
  sum_of_points = 0
  all_the_cards = []
  (File.readlines './input.txt').each do |line|
    scratcard = Scratchcard.new(line.chomp)
    all_the_cards << scratcard
    sum_of_points += scratcard.points
  end

  puts sum_of_points
  puts ScratchcardCloner.new(all_the_cards).count
end