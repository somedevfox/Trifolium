# Requiring Ruby libraries
require 'digest'
require 'securerandom'
require 'socket'

# Requiring NeoRPGServer libraries
load 'src/Logger.rb'
load 'src/SQL.rb'
load 'src/Strings.rb'
load 'src/Server.rb'

# Requiring external libraries
require 'mysql2'
puts "[+] [Library Loader] MySQL Driver has been loaded."
require 'inifile'
puts "[+] [Library Loader] IniFile library has been loaded."

NRPGS_VERSION = :NRPGSv1

module NeoRPGServer
    @configuration = {};

    #------------------------------------------------#
    # Starts Neo Server.
    #------------------------------------------------#
    def self.Run
        NeoRPGServer::Logger.Log(NeoRPGServer::Strings::PREFIX_Server, "Starting server.")

        while true
            if File.exists?('configuration.ini')
                configfile = IniFile.load('configuration.ini')
                # NeoRPGServer::Logger.LogSuccess(NeoRPGServer::Strings::PREFIX_ConfigLoader, "File loaded successfully.")
                sqldata = configfile["SQL"]
                @configuration[:SQL] = sqldata
                @configuration[:Server] = configfile["Server Info"]
                @server = NeoRPGServer::Server.new(@configuration)
            else 
                NeoRPGServer::Logger.LogError(NeoRPGServer::Strings::PREFIX_ConfigLoader, "File doesn't exist. Shutting down server...")
                abort()
            end
        end
    end


    
end

begin
    NeoRPGServer.Run
rescue Interrupt
    NeoRPGServer::Logger.LogSuccess(NeoRPGServer::Strings::PREFIX_Server, "CTRL+C received. Shutting down server...")
end