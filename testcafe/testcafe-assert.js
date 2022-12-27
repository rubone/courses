import { Selector } from 'testcafe';

fixture`Assertion Structure`
    .page`https://devexpress.github.io/testcafe/example/`;

test('Check property of element', async t => {
    const developerNameInput = Selector('#developer-name');

    await t
        .expect(developerNameInput.value).eql('', 'input is empty')
        .typeText(developerNameInput, 'Peter Parker')
        .expect(developerNameInput.value).contains('Peter', 'input contains text "Peter"');
});