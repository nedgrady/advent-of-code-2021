require_relative 'solution'



RSpec.describe 'solution' do
  it 'Does a thing' do
    schematic_raw_data = [
      '...*467...'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_part_numbers).to eq(467)
  end

  it 'Does a thing 2' do
    schematic_raw_data = [
      '...*999...'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_part_numbers).to eq(999)
  end

  it 'Does a thing 3' do
    schematic_raw_data = [
      '467..114..',
      '...*......',
      '..35..633.',
      '......#...',
      '617*......',
      '.....+.58.',
      '..592.....',
      '......755.',
      '...$.*....',
      '.664.598..'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_part_numbers).to eq(4361)
  end


  it 'Number on the far right isnt picked up' do
    schematic_raw_data = [
      '.............+........',
      '.............851../...',
      '*503...832.........219',
      '.......%...708.534....',
      '....55........*.....74',
      '......*...............',
      '.....396......501.....'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_part_numbers).to eq(851 + 503 + 832 + 219 + 708 + 534 + 55 + 396)
  end

  it 'Part 2' do
    schematic_raw_data = [
      '467..114..',
      '...*......',
      '..35..633.',
      '......#...',
      '617*......',
      '.....+.58.',
      '..592.....',
      '......755.',
      '...$.*....',
      '.664.598..'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(467835)
  end

  it 'Part north' do
    schematic_raw_data = [
      '467.114..',
      '...*......',
      '..........'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(467 * 114)
  end

  it 'Full north' do
    schematic_raw_data = [
      '4673114..',
      '...*......',
      '..........'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(0)
  end

  it 'South' do
    schematic_raw_data = [
      '..........',
      '...*......',
      '422.111...'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(422 * 111)
  end

  it 'FULL South' do
    schematic_raw_data = [
      '..........',
      '...*......',
      '4223111...'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(0)
  end

  it 'FULL South FULL north' do
    schematic_raw_data = [
      '4223111....',
      '...*.......',
      '4223111....'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(4223111 * 4223111)
  end

  it 'East West' do
    schematic_raw_data = [
      '..........',
      '.12*13....',
      '..........'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(12 * 13)
  end

  it 'One digit numbers' do
    schematic_raw_data = [
      '..........',
      '.2*1...  .',
      '..........'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(2 * 1)
  end

  it 'One digit numbers' do
    schematic_raw_data = [
      '...1......',
      '..*....  .',
      '.4........'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(4)
  end

  
  it 'One digit numbers' do
    schematic_raw_data = [
      '.8.8......',
      '..*....  .',
      '..........'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(8 * 8)
  end

    
  it 'One digits side by side' do
    schematic_raw_data = [
      '....9*9...',
      '..........',
      '..........'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(9 * 9)
  end

  it 'One digits top by bottom', :only do
    schematic_raw_data = [
      '..........',  
      '.....9....',
      '.....*....',
      '.....9....',
      '..........'
    ]

    schematic = Schematic.new(schematic_raw_data)

    expect(schematic.sum_of_gear_ratios).to eq(9 * 9)
  end
end 
