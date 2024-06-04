import matplotlib.pyplot as plt

def calculate_char_frequency(text):
    char_frequency = {}
    for char in text:
      if char != ' ' and char != '\n' and char != '\t':
        if char in char_frequency:
            char_frequency[char] += 1
        else:
            char_frequency[char] = 1
    return char_frequency

def plot_histogram(char_frequency):
    sorted_char_frequency = sorted(char_frequency.items(), key=lambda x: x[1], reverse=True)
    characters = [item[0] for item in sorted_char_frequency]
    frequencies = [item[1] for item in sorted_char_frequency]

    plt.figure(figsize=(10, 6))
    plt.bar(characters, frequencies)
    plt.xlabel('Символы')
    plt.ylabel('Частота появления')
    plt.title('Гистограмма частоты появления символов')
    plt.show()


    # Запросить у пользователя путь к файлу
file_path = 'D:/Univer/IB/3/output_base64.txt'
    # Чтение текста из файла
with open(file_path, 'r', encoding='utf-8') as file:
        text = file.read()
    # Подсчет частоты вхождения каждого символа
char_frequency = calculate_char_frequency(text)
    # Построение гистограммы
plot_histogram(char_frequency)

file_path = 'D:/Univer/IB/3/input.txt'
    # Чтение текста из файла
with open(file_path, 'r', encoding='utf-8') as file:
        text = file.read()
    # Подсчет частоты вхождения каждого символа
char_frequency = calculate_char_frequency(text)
    # Построение гистограммы
plot_histogram(char_frequency)