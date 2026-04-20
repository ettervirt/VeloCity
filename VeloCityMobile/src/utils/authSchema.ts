import { z } from 'zod';

export const loginSchema = z.object({
  email: z.email({
    error: (issue) =>
      issue.input === undefined
        ? 'E-mail jest wymagany'
        : 'To nie jest poprawny adres e-mail',
  }),
  password: z
    .string()
    .min(6, { message: "Hasło musi mieć minimum 6 znaków" }),
});

export type LoginInput = z.infer<typeof loginSchema>;
