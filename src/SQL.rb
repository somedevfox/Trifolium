module NeoRPGServer
    class SQL
        def initialize
            @sql = Mysql2::Client.new(
                :host => 'localhost',
                :username => 'root',
                :password => 'av2808',
                :database => 'NeoRPGServer'
            )
            @mutex = Mutex.new
        end

        def Disconnect
            @mutex.synchronize {
                NeoRPGServer::Logger.Log(NeoRPGServer::Strings::PREFIX_Database, "Disconnecting from MySQL Server...")
                @sql.close 
                @sql = nil
                NeoRPGServer::Logger.LogSuccess(NeoRPGServer::Strings::PREFIX_Database, "Disconnected.")
                abort()
            }
        end

        #------------------------------------------------#
        # SELECT data from Database.
        # Arguments:
        #  data - String containing data you want to get.
        #  table - String containing table name(like, users)
        #  where - (OPTIONAL) String containing where query in format: key=value (for example: userid=19231)
        # Returns: SQL query result string.
        #------------------------------------------------#
        def GetData(data, table, where=nil)
            if where == nil
                return @sql.query("SELECT #{data} FROM #{users}")
            else 
                return @sql.query("SELECT #{data} FROM #{users} WHERE #{where}")
            end
        end
        #------------------------------------------------#
        # Sends query to Database.
        # Arguments:
        #  query_string - Query string.
        # Returns SQL Query result string.
        #------------------------------------------------#
        def Query(query_string)
            return @sql.query(query_string)
        end
        #------------------------------------------------#
        # Escapes SQL string.
        # Arguments: 
        #  string - String which needs to be escaped.
        # Returns: Escaped string.
        #------------------------------------------------#
        def escape_string(string)
            return @sql.escape(string)
        end
    end
end