require 'humanize'

def get_calibration_value(calibration_line)
  digits_as_words = (1..9).map { |number| { word: number.humanize, number: number, word_reverse: number.humanize.reverse } }

  mapping_hash = {}

  (1..9).each do |num|
    word = num.humanize
    reverse_word = word.reverse
    mapping_hash[num] = num
    mapping_hash[num.to_s] = num
    mapping_hash[word] = num
    mapping_hash[reverse_word] = num
  end

  puts mapping_hash

  digits_as_words_regex = digits_as_words.map { |digit| digit[:word] }.join('|')
  digits_as_words_reverse_regex = digits_as_words.map { |digit| digit[:word_reverse] }.join('|')

  puts calibration_line

  find_the_first_number = /^.*(\d|#{digits_as_words_regex})/
  first_number_match = calibration_line.match(find_the_first_number)[1]
  first_number = mapping_hash[first_number_match]

  puts first_number_match


  find_the_first_number = /^.*(\d|#{digits_as_words_reverse_regex})/
  last_number_match = calibration_line.reverse.match(find_the_first_number)[1]
  last_number = mapping_hash[last_number_match]

  puts last_number_match
  (last_number.to_s + first_number.to_s).to_i
end

if __FILE__ == $0
  sum_of_calibration_values = 0
  (File.readlines './input.txt').each do |line|
    sum_of_calibration_values += get_calibration_value(line)
  end
  puts sum_of_calibration_values

end