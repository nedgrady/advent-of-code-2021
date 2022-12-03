# frozen_string_literal: true
def read_navigation_sub_system_output()
  File.readlines("input.txt")
end

$closing_character_map = {
    "<" => ">",
    "(" => ")",
    "[" => "]",
    "{" => "}",
}

def closing_chunks_required_to_complete_entry(navigation_entry)
  return [] if score_illegal_navigation_entry(navigation_entry) != 0

  parse_stack = []
  navigation_entry.each_char do |nav_char|
    if(is_opening(nav_char))
      parse_stack.push(nav_char)
    else
      parse_stack.pop()
    end
  end
  return parse_stack.map{|incomplete_closing_char| $closing_character_map[incomplete_closing_char]}
end


def score_illegal_navigation_entry(naviagtion_entry)
  parse_stack = []
  naviagtion_entry.each_char do |nav_char|
    if(is_opening(nav_char))
      parse_stack.push(nav_char)
    else
      opening_character = parse_stack.last()
      if(do_opening_and_closing_match?(opening_character, nav_char))
        parse_stack.pop()
      else
        return score_character(nav_char)
      end
    end
  end
  return 0
end

def score_character(nav_char)
  closing_character_score_map = {
      ">" => 25137,
      ")" => 3,
      "]" => 57,
      "}" => 1197
  }
  closing_character_score_map.default = 0

  return closing_character_score_map[nav_char]
end

def is_opening(nav_char)
  return ['<', '(', "{", "["].include? nav_char
end

def do_opening_and_closing_match?(opening_char, closing_char)
  return closing_char == $closing_character_map[opening_char]
end


navigation_entries = read_navigation_sub_system_output()

navigation_entries.each do |nav_line|
  puts(closing_chunks_required_to_complete_entry(nav_line).join())
end
