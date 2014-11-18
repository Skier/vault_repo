dropdb -U postgres llsvc
createdb -U postgres -O llsvc -E unicode -T template_postgis llsvc
psql -U llsvc -d llsvc -f 0001-schema.sql >.out 2>.err
psql -U llsvc -d llsvc -f 0002-data.sql >>.out 2>>.err

