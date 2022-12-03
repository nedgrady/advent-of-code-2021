def read_navigation_sub_system_output()
    File.readlines("input.txt")
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
    closing_character_map = {
        "<" => ">",
        "(" => ")",
        "[" => "]",
        "{" => "}",
    }

    return closing_char == closing_character_map[opening_char]
end

navigation_entries = read_navigation_sub_system_output()

total_score = 0

navigation_entries.each do |nav_line|
    total_score += score_illegal_navigation_entry(nav_line)
end

puts("Total score is #{total_score}")