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
end 
