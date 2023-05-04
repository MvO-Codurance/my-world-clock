module.exports = {
  overrides: [
    {
      files: [ "*.ts" ],
      env: {
        browser: true,
        es2021: true,
        jest: true
      },
      extends: [
        "eslint:recommended",
        "plugin:@typescript-eslint/recommended",
        "plugin:@typescript-eslint/eslint-recommended",
        "plugin:@angular-eslint/recommended",
        "plugin:import/recommended",
        "plugin:import/typescript",
        "plugin:rxjs/recommended"
      ],
      parser: "@typescript-eslint/parser",
      parserOptions: {
        ecmaVersion: 12
      },
      plugins: [
        "@typescript-eslint",
        "simple-import-sort",
        "eslint-plugin-jest",
        "unused-imports"
      ],
      rules: {
        "no-console": "error",
        "@typescript-eslint/no-unused-vars": ["off"],
        "unused-imports/no-unused-imports": "error",
        "unused-imports/no-unused-vars": "error",
        "@typescript-eslint/no-explicit-any": 2,
        "semi": ["error", "never"],
        "@typescript-eslint/indent": ["error", 2, {
          "ignoredNodes": [
            "PropertyDefinition[decorators]",
            "TSUnionType"
          ]
        }],
        "no-multiple-empty-lines": ["error", { "max": 1, "maxEOF": 1 }],
        "brace-style" : "error",
        "object-curly-newline": ["error", {
          "ObjectExpression": "always",
          "ObjectPattern": { "multiline": true },
          "ImportDeclaration": { "multiline": true },
          "ExportDeclaration": { "multiline": true }
        }],
        "object-curly-spacing": ["error", "always"],
        "keyword-spacing": ["error"],
        "no-spaced-func" : ["error"],
        "quotes": ["error", "single"],
        "space-before-function-paren" : ["error", {"anonymous": "always", "named": "never", "asyncArrow": "always"}],
        "max-len": ["error", { "code": 120, ignorePattern: '^import .*' }]
      }
    },
    {
      files: ["**/*.spec.ts"],
      parser: "@typescript-eslint/parser",
      env: {
        jest: true
      },
      rules: {
        "jest/no-disabled-tests": "warn",
        "jest/no-focused-tests": "error",
        "jest/no-identical-title": "error",
        "jest/prefer-to-have-length": "warn",
        "@typescript-eslint/no-empty-function": "off",
        "@typescript-eslint/no-non-null-assertion": "off"
      }
    },
    {
      files: ["./setup-jest.ts"],
      parser: "@typescript-eslint/parser",
      env: {
        jest: true
      },
      rules: {
        "@typescript-eslint/no-explicit-any": 0
      }
    },
    {
      files: ["*.component.html"],
      parser: "@angular-eslint/template-parser",
      plugins: ["@angular-eslint/template"],
      extends: ["plugin:@angular-eslint/template/recommended"],
      rules: {}
    },

  ],
  settings: {
    "import/resolver": {
      typescript: {} // this loads <rootdir>/tsconfig.json to eslint
    },
  },
  extends: ["plugin:storybook/recommended"]
}
