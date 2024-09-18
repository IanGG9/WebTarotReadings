import pyodbc
import pandas as pd
import sys

sys.stdout.reconfigure(encoding='utf-8')
 
def read_excel_file(file_path, table_name):
    # Load Excel data 
    table_data = pd.read_excel(file_path, sheet_name=table_name)
    return table_data



def connect_to_database():
    connection_string = (
        'DRIVER=SQL Server;'
        'SERVER=DESKTOP-EC3DFK4;'
        'DATABASE=WebTarotDb;'
        'Trusted_Connection=yes;'
    )
 
    try:
        connection = pyodbc.connect(connection_string)
        print("Connected to the database.")
        return connection
    except Exception as e:
        print(f"Failed to connect to the database: {e}")
        return None
    

    
def create_table(table_name, table_data):
    connection = connect_to_database()
    if connection is None:
        return
    
    table_name_safe = f"[{table_name}]"
    columns = [f"[{col}]" for col in table_data.columns]

    cursor = connection.cursor()
    
    if table_name == "TarotCards":
        sql_query = f"""CREATE TABLE {table_name_safe} (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            {table_data.columns[0]} VARCHAR(50),
            {table_data.columns[1]} BIT,
            {table_data.columns[2]} VARCHAR(MAX)
        )"""
    else:
        sql_query = f"""CREATE TABLE {table_name_safe} (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            {table_data.columns[0]} VARCHAR(20),
            {table_data.columns[1]} VARCHAR(6),
            {table_data.columns[2]} VARCHAR(6)
        )"""

    try:
        cursor.execute(sql_query)
        connection.commit() 
    except Exception as e:
        print(f"Error while creating table: {e}")
        connection.rollback() 
    finally:
        cursor.close()
        connection.close()
 

def insert_data_from_excel(table_name, table_data):
    # Read Excel data into a DataFrame
    connection = connect_to_database()
    if connection is None:
        return

    columns = [f"[{col}]" for col in table_data.columns]
    cursor = connection.cursor()

    # Iterate over the rows of the DataFrame and insert them into the database
    for index, row in table_data.iterrows():
        try:
            # Construct the SQL query with placeholders for each value
            sql = f"""INSERT INTO {table_name} ({columns[0]}, {columns[1]}, {columns[2]})
                    VALUES (?, ?, ?)"""
            # Execute the query with the values from the current row
            cursor.execute(sql, (row[table_data.columns[0]], 
                                row[table_data.columns[1]], 
                                row[table_data.columns[2]]))
            

        except Exception as e:
            print(f"Error inserting row {index + 1}: {e}")

    # Commit the transaction and close the connection
    connection.commit()
    cursor.close()
    connection.close()
    print("Data inserted successfully.")


def main():
    excel_file = "C:\\Users\\Korisnik\\Desktop\\WebTarotReading\\tarot_and_horoscope.xlsx"
    table_names = ["TarotCards", "HoroscopeSigns"] #Input as many tables names as you need
    tables_data = [] #Here will be saved all data from each table in 'table_names'
 
    # Read the Excel data

    for table_name in table_names:
        tables_data.append(read_excel_file(excel_file, table_name))
    # print(tables_data)

    # for i in range(len(table_names)):
    #     create_table(table_names[i], tables_data[i])
 
    # Connect to the database
    connection = connect_to_database()
    if connection:
        # Insert the data into the database
        for i in range(len(table_names)):
            insert_data_from_excel(table_names[i], tables_data[i])
 
        # Close the database connection
        connection.close()
        print("Database connection closed.")
 

excel_file = "C:\\Users\\Korisnik\\Desktop\\WebTarotReading\\tarot_and_horoscope.xlsx"
table_names = ["TarotCards", "HoroscopeSigns"] #Input as many tables names as you need
tables_data = [] #Here will be saved all data from each table in 'table_names'

# Read the Excel data

for table_name in table_names:
    tables_data.append(read_excel_file(excel_file, table_name))
# print(tables_data)

# for i in range(len(table_names)):
#     create_table(table_names[i], tables_data[i])

# Connect to the database
connection = connect_to_database()
if connection:
    # Insert the data into the database
    for i in range(len(table_names)):
        insert_data_from_excel(table_names[i], tables_data[i])

    # Close the database connection
    connection.close()
    print("Database connection closed.")
