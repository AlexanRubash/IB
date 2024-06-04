import base64

def xor_buffers(a, b):
    if len(a) != len(b):
        raise ValueError("Размеры буферов должны быть одинаковыми")

    a_nums = [ord(c) for c in a]
    b_nums = [ord(c) for c in b]

    result_nums = [x ^ y ^ y for x, y in zip(a_nums, b_nums)]

    # Преобразуем числа ASCII обратно в строку
    result = ''.join(chr(num) for num in result_nums)

    return result


buffer_a = "Rubashek"
buffer_b = "Alexandr"
result = xor_buffers(buffer_a, buffer_b)
print("Результат (ASCII):", result)

buffer_a_base64 = base64.b64encode(buffer_a.encode()).decode()
print("Результат base64(a):",buffer_a_base64)
buffer_b_base64 = base64.b64encode(buffer_b.encode()).decode()
result_base64 = xor_buffers(buffer_a_base64, buffer_b_base64)
print("Результат base64(a XOR b XOR b):", result_base64)