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

  def sum_of_gear_ratios
    running_sum_of_gear_ratios = 0
    for y in 0..@schematic_raw_data.length - 1
      for x in 0..@schematic_raw_data[y].length - 1
          running_sum_of_gear_ratios += gear_ratio(x, y)
      end
    end

    running_sum_of_gear_ratios
  
  end

  def gear_ratio(x, y)
    # if the current chacater is a '*' AND contains exactly two adjacent numbers
    # then return the ratio of those two numbers
    if @schematic_raw_data[y][x] == '*'
      adjacent_numbers = adjacent_numbers(x, y)
      if adjacent_numbers.length == 2
        puts "Found a gear ratio: #{adjacent_numbers[0]} * #{adjacent_numbers[1]} at #{x}, #{y}"
        return adjacent_numbers[0] * adjacent_numbers[1]
      end
    end
    0
  end
end

  def adjacent_numbers(x, y)
    adjacent_numbers = []
    
    # check the row above, left side for a number
    current_thing_to_check = north_west(x, y)
    north_west_number_as_string = ''
    unless current_thing_to_check.nil?
      # while we're on a number
      while (current_thing_to_check != nil && current_thing_to_check[:value] =~ /\d/)
        north_west_number_as_string += current_thing_to_check[:value]
        current_thing_to_check = west(current_thing_to_check[:x], current_thing_to_check[:y])
      end
    end

    north_west_number_as_string = north_west_number_as_string.reverse

    current_thing_to_check = north_east(x, y)
    north_east_number_as_string = ''
    unless current_thing_to_check.nil?
      # while we're on a number
      while(current_thing_to_check != nil && current_thing_to_check[:value] =~ /\d/)
        north_east_number_as_string += current_thing_to_check[:value]
        current_thing_to_check = east(current_thing_to_check[:x], current_thing_to_check[:y])
      end
    end

    if(north(x, y)[:value] =~ /\d/)
      puts 'here'
      spanning_northern_number_as_string = "#{north_west_number_as_string}#{north(x, y)[:value]}#{north_east_number_as_string}"
      puts spanning_northern_number_as_string
      adjacent_numbers.push(spanning_northern_number_as_string.to_i) unless spanning_northern_number_as_string.empty?
    else
      adjacent_numbers.push(north_west_number_as_string.to_i) unless north_west_number_as_string.empty?
      adjacent_numbers.push(north_east_number_as_string.to_i) unless north_east_number_as_string.empty?
    end


    current_thing_to_check = south_west(x, y)
    south_west_number_as_string = ''
    unless current_thing_to_check.nil?
      # while we're on a number
      while current_thing_to_check != nil && current_thing_to_check[:value] =~ /\d/
        south_west_number_as_string += current_thing_to_check[:value]
        current_thing_to_check = west(current_thing_to_check[:x], current_thing_to_check[:y])
      end
    end

    south_west_number_as_string = south_west_number_as_string.reverse

    current_thing_to_check = south_east(x, y)
    south_east_number_as_string = ''
    unless current_thing_to_check.nil?
      # while we're on a number
      while(current_thing_to_check != nil && current_thing_to_check[:value] =~ /\d/)
        south_east_number_as_string += current_thing_to_check[:value]
        current_thing_to_check = east(current_thing_to_check[:x], current_thing_to_check[:y])
      end
    end

    if(south(x, y)[:value] =~ /\d/)
      puts 'hehre 2'
      spanning_southern_number_as_string = "#{south_west_number_as_string}#{south(x, y)[:value]}#{south_east_number_as_string}"
      puts spanning_southern_number_as_string
      adjacent_numbers.push(spanning_southern_number_as_string.to_i) unless spanning_southern_number_as_string.empty?
    else
      adjacent_numbers.push(south_west_number_as_string.to_i) unless south_west_number_as_string.empty?
      adjacent_numbers.push(south_east_number_as_string.to_i) unless south_east_number_as_string.empty?
    end

    puts 'hereee'
    puts adjacent_numbers.length


    current_thing_to_check = west(x, y)
    west_number_as_string = ''
    unless current_thing_to_check.nil?
      # while we're on a number
      while (current_thing_to_check != nil && current_thing_to_check[:value] =~ /\d/)
        west_number_as_string += current_thing_to_check[:value]
        current_thing_to_check = west(current_thing_to_check[:x], current_thing_to_check[:y])
      end
    end

    west_number_as_string = west_number_as_string.reverse
    adjacent_numbers.push(west_number_as_string.to_i) unless west_number_as_string.empty?

    current_thing_to_check = east(x, y)
    east_number_as_string = ''
    unless current_thing_to_check.nil?
      # while we're on a number
      while(current_thing_to_check != nil && current_thing_to_check[:value] =~ /\d/)
        east_number_as_string += current_thing_to_check[:value]
        current_thing_to_check = east(current_thing_to_check[:x], current_thing_to_check[:y])
      end
    end

    adjacent_numbers.push(east_number_as_string.to_i) unless east_number_as_string.empty?
    adjacent_numbers
  end

  def north_west(x, y)
    if (x > 0 && y > 0)
      return { value: @schematic_raw_data[y - 1][x - 1], x: x - 1, y: y - 1 }
    end
    return nil
  end

  def north(x, y)
    if (y > 0)
      return { value: @schematic_raw_data[y - 1][x], x: x, y: y - 1 }
    end

    return nil
  end

  def west(x, y)
    if (x > 0)
      return { value: @schematic_raw_data[y][x - 1], x: x - 1, y: y }
    end

    return nil
  end

  def north_east(x, y)
    if (x < @schematic_raw_data[y].length - 1 && y > 0)
      return { value: @schematic_raw_data[y - 1][x + 1], x: x + 1, y: y - 1 }
    end

    return nil
  end

  def east(x, y)
    if (x < @schematic_raw_data[y].length - 1)
      return { value: @schematic_raw_data[y][x + 1], x: x + 1, y: y }
    end

    return nil
  end

  def south_west(x, y)
    if (x > 0 && y < @schematic_raw_data.length - 1)
      return { value: @schematic_raw_data[y + 1][x - 1], x: x - 1, y: y + 1 }
    end

    return nil
  end

  def south(x, y)
    if (y < @schematic_raw_data.length - 1)
      return { value: @schematic_raw_data[y + 1][x], x: x, y: y + 1 }
    end

    return nil
  end

  def south_east(x, y)
    if (x < @schematic_raw_data[y].length - 1 && y < @schematic_raw_data.length - 1)
      return { value: @schematic_raw_data[y + 1][x + 1], x: x + 1, y: y + 1 }
    end

    return nil
  end

if __FILE__ == $0
  schematic = Schematic.new (File.readlines './input.txt', chomp: true)

  puts schematic.sum_of_part_numbers
  puts schematic.sum_of_gear_ratios

end