module NeoRPGServer
    module Logger
        #------------------------------------------------#
        # Prints out message and puts it in file.
        # Requires: 
        #   prefix - String containing prefix of script/worker.
        #   message - String containing message.
        #------------------------------------------------#
        def self.Log(prefix, message)
            puts "[ ] [#{prefix}] #{message}"
        end
        #------------------------------------------------#
        # Prints out success message and puts it in file.
        # Requires: 
        #   prefix - String containing prefix of script/worker.
        #   successmsg - String containing message.
        #------------------------------------------------#
        def self.LogSuccess(prefix, successmsg)
            puts "[+] [#{prefix}] #{successmsg}".green
        end
        #------------------------------------------------#
        # Prints out error message, puts it in file(main.log & error.log) and prints out it in Discord.
        # Requires: 
        #   prefix - String containing prefix of script/worker.
        #   error - String containing error message.
        #------------------------------------------------#
        def self.LogError(prefix, error)
            puts "[-] [#{prefix}] #{error}".red.italic
        end
    end
end