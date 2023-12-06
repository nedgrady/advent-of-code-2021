require 'humanize'

class Schematic
  def initialize(schematic_raw_data)
    @schematic_raw_data = schematic_raw_data
  end

  def sum_of_part_numbers
    running_sum_of_part_numbers = 0
    y = 0
    while y < @schematic_raw_data.length
      x = 0
      while x < @schematic_raw_data[y].length
        current_chunk = chunk(x, y)
        if current_chunk.is_a?(Integer)
          # puts "Found a valid part number: #{current_chunk}"
          running_sum_of_part_numbers += current_chunk
          x += current_chunk.to_s.length
        else
          x += 1
        end
      end
      y += 1
    end

    running_sum_of_part_numbers
  end

  def chunk(x, y)
    # are we currently on a digits?

    if @schematic_raw_data[y][x] =~ /\d/
      current_numbber_as_string = @schematic_raw_data[y][x]
      # find the adjacent items
      far_left = [x - 1, 0].max
      
      x += 1
      while(@schematic_raw_data[y][x] =~ /\d/)
        current_numbber_as_string += @schematic_raw_data[y][x]
        x += 1
      end

      far_right = [x, @schematic_raw_data[y].length - 1].min

      top_left = { x: far_left, y: [y - 1, 0].max }
      bottom_right = { x: far_right, y: [y + 1, @schematic_raw_data.length - 1].min }

      dbg = nil
      if(current_numbber_as_string == '677')
        puts "Found 677"

        puts "checking bounds #{top_left} #{bottom_right}"
        dbg = true
      end

      return current_numbber_as_string.to_i if any_symbols_present_in_rectangle?(top_left, bottom_right, dbg)
    end

    false
  end

  def any_symbols_present_in_rectangle?(top_left, bottom_right, dbg)
    for y in top_left[:y]..bottom_right[:y]
      for x in top_left[:x]..bottom_right[:x]
        puts "checking #{x}, #{y} #{@schematic_raw_data[y][x]}" if dbg
        if is_symbol @schematic_raw_data[y][x]
          puts "found #{x}, #{y} '#{@schematic_raw_data[y][x].ord}'" if dbg
          return true
        end
      end
    end

    false
  end

  def is_symbol(character)
    character =~ /\D/ && character != '.'
  end
end

if __FILE__ == $0
  schematic = Schematic.new (File.readlines './input.txt', chomp: true)

  puts schematic.sum_of_part_numbers

end