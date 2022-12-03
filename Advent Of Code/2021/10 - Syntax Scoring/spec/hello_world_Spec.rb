require 'fileutils'

def read_navigation_sub_system_output
  File.readlines("input.txt")
end

# describe "read_navigation_sub_system_output" do
#     [['[(<>)]', '((())', '[]]]]]]'], ['<><><><>', '<<<', '[]]]]]]']].each do |fileContents|
#       it "should reflect the file contents" do
#         fileContents.each do |line|
#           File.write('input.txt', line)
#         end

#         navigation_lines = read_navigation_sub_system_output()

#         navigation_lines.each do |line|
#           expect(line).to eq fileContents
#         end

#         FileUtils.remove('input.txt')
#       end
#     end

#   before do
#       @old_pwd = Dir.pwd
#       FileUtils.mkdir_p("temporary directory for tests")
#       Dir.chdir("temporary directory for tests")
#   end
    
#   after do
#       Dir.chdir(@old_pwd)
#       FileUtils.rm_rf("temporary directory for tests")
#   end
# end

describe read_navigation_sub_system_output do
  it "should reflect the file contents" do
    FileUtils.remove('input.txt')
    File.open('input.txt', 'a+')
    File.write('input.txt', "[][][]")

    navigation_lines = read_navigation_sub_system_output()

    navigation_lines.each do |line|
      expect(line).to eq fileContents
    end
    FileUtils.remove('input.txt')
  end
end
