import base64

def encode_to_base64(input_file, output_file):
    try:
        # Открываем файл для чтения
        with open(input_file, 'rb') as f:
            # Считываем данные из файла
            data = f.read()

            # Приводим содержимое файла к нижнему регистру
            data_lower = data.lower()
            data_lower = data_lower.replace(b' ', b'')

            # Кодируем данные в формат Base64
            encoded_data = base64.b64encode(data_lower)

            # Открываем файл для записи
            with open(output_file, 'wb') as f_out:
                # Записываем закодированные данные в файл
                f_out.write(encoded_data)

            print("Файл успешно закодирован в формат Base64 и сохранен в", output_file)
    except Exception as e:
        print("Произошла ошибка:", e)


# Пример вызова функции
encode_to_base64("input.txt", "output_base64.txt")
