# Requiring Ruby libraries
require 'digest'
require 'securerandom'
require 'socket'

# Requiring NeoRPGServer libraries
load 'src/Logger.rb'
load 'src/SQL.rb'
load 'src/Strings.rb'
load 'src/Server.rb'
load 'src/ClientManager.rb'
load 'src/Client.rb'
load 'src/Commands.rb'

# Requiring external libraries
require 'mysql2'
puts "[+] [Library Loader] MySQL Driver has been loaded."
require 'inifile'
puts "[+] [Library Loader] IniFile library has been loaded."
require 'colorize'
puts "[+] [Library Loader] colorize library has been loaded."

# Constants
NRPGS_VERSION = :NRPGSv1

# Run Splash
for l in Splash2 do; puts l.light_magenta ;end
puts "\nNeoRPGServer #{NRPGS_VERSION}. Written by `Delight-GameStudios` and `Eternabyte`.\n\n\n".green.underline

module NeoRPGServer
    $configuration = {};

    #------------------------------------------------#
    # Starts Neo Server.
    #------------------------------------------------#
    def self.Run
        NeoRPGServer::Logger.Log(NeoRPGServer::Strings::PREFIX_Server, "Starting server.")

        if File.exists?('configuration.ini')
            @configfile = IniFile.load('configuration.ini')
            # NeoRPGServer::Logger.LogSuccess(NeoRPGServer::Strings::PREFIX_ConfigLoader, "File loaded successfully.")
            sqldata = @configfile["SQL"]
            @configuration[:SQL] = sqldata
            @configuration[:Server] = @configfile["Server Info"]
            # puts @configuration[:Server]
            $ClientManager = NeoRPGServer::ClientManager.new
            @server = NeoRPGServer::Server.new(@configuration)
        else 
            NeoRPGServer::Logger.LogError(NeoRPGServer::Strings::PREFIX_ConfigLoader, "File doesn't exist. Shutting down server...")
            abort()
        end
        #while true
            
        #end
    end


    
end

begin
    NeoRPGServer.Run
rescue Interrupt
    NeoRPGServer::Logger.LogSuccess(NeoRPGServer::Strings::PREFIX_Server, "CTRL+C received. Shutting down server...")
#rescue NoMemoryError, ScriptError, StandardError, NameError => error
    #puts error.to_s.red.underline.bold.italic
end